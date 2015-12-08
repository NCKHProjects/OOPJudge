<?php
    
    $file = "tmp.zip";
    //if (!file_exists($file)) {
        $zip = new ZipArchive;
        $res = $zip->open($file, ZipArchive::CREATE);
        $zip->addEmptyDir("Lab");
        
        for($i = 0; $i <2; $i++){
            $dd = $i > 0 ? "a" : "b";
            $dir = $dd;
            $files = scandir($dir);
            foreach($files as $f){
                if(($f != ".")&&($f != "..")){
                    $srcFile = $dir."/".$f;
                    $desFile = "Lab/" . $f;
                    $r = $zip->addFile($srcFile, $desFile);
                }
            }
        }
        $r = $zip->addFile("lab.txt");
        $zip->close();
    //}

if (file_exists($file)) {
    header('Content-Description: File Transfer');
    header('Content-Type: application/octet-stream');
    header('Content-Disposition: attachment; filename='.$file);
    header('Expires: 0');
    header('Cache-Control: must-revalidate');
    header('Pragma: public');
    header('Content-Length: ' . filesize($file));
    readfile($file);
    exit;
}
?>