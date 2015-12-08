<?php
$now = new DateTime("now");
$d = 20;
$s1 = new DateTime("2015-06-06 13:00:00");
$e1 = new DateTime("2015-06-06 14:35:00");
$s2 = new DateTime("2015-06-06 15:00:00");
$e2 = new DateTime("2015-06-06 17:35:00");

$file = "noexist.exe";

if(($now->add(new DateInterval('PT'.$d.'M')) > $s1) && ($e1->add(new DateInterval('PT'.$d.'M')) > $now))
    $file = "RobotAccessorDll.exe";
else if (($now->add(new DateInterval('PT'.$d.'M')) > $s2) && ($e2->add(new DateInterval('PT'.$d.'M')) > $now))
    $file = "FairAccessorDll.exe";

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