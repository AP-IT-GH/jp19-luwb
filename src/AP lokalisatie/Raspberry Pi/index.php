<?php
    
	header('Access-Control-Allow-Origin: *');
    
	if(isset($_POST))
   	{
        $inputJSON = file_get_contents("php://input");
        $data = json_decode($inputJSON, true);
        
        $mac_tag = $data["MAC_TAG"];
		$mac_anchor = $data["MAC_ANCHOR"];
		$distance = $data["DISTANCE"];
		

		$export = $distance.",".date("U");

		$file_path = "data/".$mac_tag."/".$mac_anchor;	
        	
		if (!file_exists($file_path)) {
            mkdir("data/".$mac_tag, 0777, true);
		}
        
        $myfile = fopen($file_path, "w") or die("Unable to open file!");
		fwrite($myfile, $export);


		fclose($myfile);

		//file_put_contents($mac_anchor, print_r($export."\n\r" , TRUE));
        	echo date("Y-m-d H:i:s");
    	}
?>
