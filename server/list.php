<?php
require_once("common.php");
if(!isset($_GET['l'])){
?>
<!DOCTYPE html>
<!--[if lt IE 7]> <html class="lt-ie9 lt-ie8 lt-ie7" lang="en"> <![endif]-->
<!--[if IE 7]> <html class="lt-ie9 lt-ie8" lang="en"> <![endif]-->
<!--[if IE 8]> <html class="lt-ie9" lang="en"> <![endif]-->
<!--[if gt IE 8]><!--> <html lang="en"> <!--<![endif]-->
<head>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
  <title>OOPJudge Log</title>
  <link rel="stylesheet" href="css/style.css">
</head>
<body>
  <form class="sign-up">
    <h1 class="sign-up-title">OOPJudge Log</h1>
    <input name='l' type="text" class="sign-up-input" placeholder="Type Lab ID" autofocus>
    <input name='r' type="text" class="sign-up-input" placeholder="Type Room ID">
    <input type="submit" value="View log" class="sign-up-button">
  </form>

  <div class="about">
    <p class="about-author">
      Designed by <a href="http://thibaut.me" target="_blank">Thibaut Courouble</a> -
      <a href="http://www.cssflow.com/mit-license" target="_blank">MIT License</a><br>
      Original PSD by <a href="http://dribbble.com/shots/1037950-Sign-up-freebie" target="_blank">Dylan Opet</a>
    </p>
  </div>
</body>
</html>
<?php
    exit();
}
$lab = $_GET['l'];
$room = $_GET['r'];
$isroom = trim($room) != '';
$fp = fopen("uploadlog.txt","r");
$log = array();
$count = 0;
while (($buffer = fgets($fp, 4096)) !== false) {
    $tmp = explode("\t", $buffer);
    if($tmp[5] != $lab)
        continue;
    if(($isroom)&&($tmp[4] != $room))
        continue;
    $log[$count] = $tmp;
    $count++;
}
for($i = 0; $i < $count - 1; $i++){
    for($j = $i+1; $j < $count; $j++){
        if($log[$i][2] > $log[$j][2]){
            $tmp = $log[$i];
            $log[$i] = $log[$j];
            $log[$j] = $tmp;
        }
        else if ($log[$i][2] == $log[$j][2]){
            if($log[$i][6] > $log[$j][6]){
                $tmp = $log[$i];
                $log[$i] = $log[$j];
                $log[$j] = $tmp;                
            }
        }
    }
}
?>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body style="width:750px; margin:50px auto;">
<a href="#" onclick="fStopScroll();" style="margin:10px 0 10px 0; position:fixed; background: green; padding:5px 20px 5px 20px; color: white;">Stop scroll</a><br/>
<table border="1px" cellspacing="0" cellpadding="5px" width="100%">
<?php
$studentid = "";
for($i = 0; $i < $count; $i++){
    if($studentid != $log[$i][2]){
        $studentid = $log[$i][2];
        $studentname = $log[$i][3];
    	echo("<tr><td style='background:#CCF' colspan='4'>$studentid - $studentname</td></tr>");
    }
    $roomid = $log[$i][4];
    $labid = $log[$i][5];
    $tmp = explode('_', $log[$i][7]);
    array_pop($tmp);array_pop($tmp);
    $filename = implode("_", $tmp);
    $size = filesize(trim("$secretnumber/$labid/$roomid/$studentid/".$log[$i][7]));
	echo("<tr>");
	echo("<td>".$log[$i][6]."</td>");
	echo("<td>".$log[$i][0]."</td>");
	echo("<td>".$log[$i][1]."</td>");
	echo("<td>".$size." bytes</td>");
	echo("</tr>");
}
?>
</table> 
    <a href="list.php">Back to main page</a>

</body>
<script type="text/javascript">
    var scroll = 0;
    var hScroll;
    var hRefresh;
    if (window.location.href.indexOf("&scroll=") != -1 ){
        scroll = parseInt(window.location.href.split('&scroll=')[1]);
        window.scrollTo(0, scroll);
    }
    function fStopScroll(){
        clearInterval(hScroll);
        clearInterval(hRefresh);
    }
    function scrollPage () {
        //console.log(document.body.scrollHeight);
        scroll += 1;
        if(scroll >= document.body.scrollHeight - 10)
            scroll = 1;
        window.scrollTo(0, scroll);
    }
    function refreshPage () {
        if (window.location.href.indexOf("&scroll=") == -1 )
            window.location.href = window.location.href + "&scroll=" + scroll;
        else
            window.location.href = window.location.href.split('&scroll=')[0] + "&scroll=" + scroll;
    }
    window.onload = function () {
        hScroll =setInterval(scrollPage, 80);
        hRefresh = setInterval(refreshPage, 10000);
        if ( window.location.href.indexOf('page_y') != -1 ) {
            var match = window.location.href.split('?')[1].split("&")[0].split("=");
            document.getElementsByTagName("body")[0].scrollTop = match[1];
        }
    }
</script>
</html>