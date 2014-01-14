<?php 
//inclusуo do arquivo de classes NuSOAP
require_once('lib/nusoap.php');
//Criar de instтncia do servidor
$server = new soap_server;


$server->configureWSDL('server.autenticaUser', 'urn:server.autenticaUser');
$server->wsdl->schemaTargetNamespace='urn:server.autenticaUser';
//registro do mщtodo
$server->register('autenticaUser',
array('email'=>'xsd:integer','password'=>'xsd:integer'),
array('return'=>'xsd:integer'),
'urn:server.autenticaUser',
'urn:server.autenticaUser#autenticaUser',
'rpc',
'encoded',
'Retorna o valor'
);
//definiчуo do mщtodo como uma funчуo do PHP
function autenticaUser($email,$password)
{
return 0;
}


$HTTP_RAW_POST_DATA=isset($HTTP_RAW_POST_DATA )?$HTTP_RAW_POST_DATA: '';
$server->service($HTTP_RAW_POST_DATA);
?>