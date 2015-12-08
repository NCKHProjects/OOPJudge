<?php
if(isset($_GET['u'])){
	$v = $_GET['u'];
	if($v != "1.0.0.9")
		exit("yes");
	else if (isset($_GET['l'])){
	    $l = $_GET['l'];
	    if($l != '15'){
	        exit("lab");
	    }
	}
	exit("no");
}
?>