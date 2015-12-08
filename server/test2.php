<?php
$fp = fopen("t.txt", "a");

if (flock($fp, LOCK_EX)) {  // acquire an exclusive lock
    sleep(5);
    fwrite($fp, "456\n");
    fflush($fp);            // flush output before releasing the lock
    flock($fp, LOCK_UN);    // release the lock
} else {
    echo "Couldn't get the lock!";
}

fclose($fp);
?>