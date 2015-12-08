<?php
    $ip = "";
    if (!empty($_SERVER['HTTP_CLIENT_IP'])) {
        $ip = $_SERVER['HTTP_CLIENT_IP'];
    } elseif (!empty($_SERVER['HTTP_X_FORWARDED_FOR'])) {
        $ip = $_SERVER['HTTP_X_FORWARDED_FOR'];
    } else {
        $ip = $_SERVER['REMOTE_ADDR'];
    }
    
    if(!isset($_GET['u']))
        exit();
    if(!isset($_GET['l']))
        exit();
    require_once('common.php');
    $user = $_GET['u'];
    $username = $_GET['n'];
    $lab = $_GET['l'];
    $mtime = $_GET['m'];
    $hash = $_GET['h'];
    $room = $_GET['r'];
    
    if($_FILES['file']['error'] != UPLOAD_ERR_OK)
        exit ("Error");
    $uploaddir = $secretnumber.'/'.$lab.'/'.$room.'/'.$user.'/';
    if(!file_exists($uploaddir)){
        mkdir($uploaddir, 0777, true);
    }
    $uploadfile = $uploaddir . basename($_FILES['file']['name']);
    $pathinfo = pathinfo($uploadfile);
    $filename = $pathinfo['filename'].'_'.time();
    $ext = $pathinfo['extension'];
    if(!move_uploaded_file($_FILES['file']['tmp_name'], $uploaddir.$filename.'.'.$ext)){
        exit("FAIL");
    }
    
    $md5 = md5_file($uploaddir.$filename.'.'.$ext);
    if($hash != $md5){
        unlink($uploaddir.$filename.'.'.$ext);
        exit("FAIL");
    }
        
    $fp = fopen("uploadlog.txt", "a");
    $currentdate = date("Y-m-d H:i:s");
    $log = "$currentdate\t$ip\t$user\t$username\t$room\t$lab\t$mtime\t$filename.$ext\n";
    if (flock($fp, LOCK_EX)) {
        fwrite($fp, $log);
        fflush($fp);
        flock($fp, LOCK_UN);
    } else {
        exit("Couldn't get the lock!");
    }
    fclose($fp);
    exit("OK");
?>