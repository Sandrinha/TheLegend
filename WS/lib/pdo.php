<?php
require_once('configPdo.php');
// print_r(PDO::getAvailableDrivers()); 
// initiate database handler
try{
   $DBH = new PDO("dblib:host=$myServer;dbname=$myDB", $myUser, $myPass);
   $DBH->setAttribute( PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION );
}catch(PDOException $e){
	return new soap_fault('Server', '', "Failed to connect to database: $e->getMessage() ",'');
//echo 'Failed to connect to database: ' . $e->getMessage() . "\n";
//exit;
};
