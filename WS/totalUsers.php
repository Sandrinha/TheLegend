<?php 
//inclusão do arquivo de classes NuSOAP
require_once('lib/nusoap.php');

//Criar  instancia 
$server = new soap_server;

$server->configureWSDL('totalUsers', 'urn:totalUsers');
$server->wsdl->schemaTargetNamespace='urn:totalUsers';
//registro do metodo
$server->register(	'totalUsers',							//method name
	array(),								//input
	array('return'=>'xsd:integer'),								//output
	'urn:totalUsers',									//namespace
	'urn:totalUsers#totalUsers',						//soapaction
	'rpc',														//style
	'encoded',													//use
	'Retorna total de users'								//documentation
);
//definir metodo 
function totalUsers()
{
	//inclusão do arquivo de classes PDO
	require_once('lib/pdo.php');
	//$email='aaa@bbb.ccc';
	$sql ="SELECT count(Tag_TagID) AS 'total' FROM TheLegend.dbo.TagUserProfiles";
	$query = $DBH->prepare($sql);
    $query->execute();
    $row = $query->fetch(PDO::FETCH_ASSOC);
	//print_r($row);
	if(count ($row) > 0 ) {
		return $row["total"];
	} else {
		return new soap_fault('Server', '', 'no results to show','');
	}
}
$HTTP_RAW_POST_DATA=isset($HTTP_RAW_POST_DATA )?$HTTP_RAW_POST_DATA: '';
$server->service($HTTP_RAW_POST_DATA);

?>