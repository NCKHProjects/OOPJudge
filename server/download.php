<?php
if(!isset($_GET['type']))
    exit();
$type = $_GET['type'];

$file = "OOPJudge.zip";
$filename = "OOPJudge.zip";

if($type != '0'){
    $filename = "Lab.zip";
    $ver = intval($type);
    $fTemp = fopen("lab.txt","r");
    $lastVer = intval(fread($fTemp,filesize("lab.txt")));
    fclose($fTemp);
    
    $file = "Lab".$lastVer.".".$ver.".zip";
    if (!file_exists($file)) {
        $zip = new ZipArchive;
        $res = $zip->open($file, ZipArchive::CREATE);
        $zip->addEmptyDir("Lab");
        
        for($i = $ver + 1; $i <= $lastVer; $i++){
            $dir = "Lab".$i;
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
    }
}

if (file_exists($file)) {
    header('Content-Description: File Transfer');
    header('Content-Type: application/octet-stream');
    header('Content-Disposition: attachment; filename='.$filename);
    header('Expires: 0');
    header('Cache-Control: must-revalidate');
    header('Pragma: public');
    header('Content-Length: ' . filesize($file));
    readfile($file);
    exit;
}
?>