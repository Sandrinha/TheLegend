<?php 
//inclusão do arquivo de classes NuSOAP
require_once('lib/nusoap.php');

//Criar  instancia 
$server = new soap_server();

$server->configureWSDL('tags', 'urn:tags');
$server->wsdl->schemaTargetNamespace='urn:tags';

$server->wsdl->addComplexType(
    'tagsLines',
    'complexType',
    'struct',
    'all',
    '',
    array(
        'TagID' => array('name' => 'TagID', 'type' => 'xsd:integer'),
        'TagName' => array('name' => 'TagName', 'type' => 'xsd:string')
    )
);
$server->wsdl->addComplexType(
        'tagsArray',// name  
        'complexType',// typeClass (complexType|simpleType|attribute)
        'array',// phpType: currently supported are array and struct (php assoc array)  
        '',// compositor (all|sequence|choice)
        'SOAP-ENC:Array',// restrictionBase namespace:name (http://schemas.xmlsoap.org/soap/encoding/:Array)
        array(),// elements = array ( name = array(name=>'',type=>'') )
        array(// attrs
            array(
                'ref' => 'SOAP-ENC:arrayType',
                'wsdl:arrayType' => 'tns:tagsLines[]'
            )
        ),
        'tns:tagsLines'// arrayType: namespace:name (http://www.w3.org/2001/XMLSchema:string)
    );

//registro do metodo
$server->register(	'listaTags',							//method name
	array(),								//input
	array('return'=>'tns:tagsArray'),								//output
	'urn:tags',									//namespace
	'urn:tags#listaTags',						//soapaction
	'rpc',														//style
	'encoded',													//use
	'Retorna as tags disponiveis'								//documentation
);

//registro do metodo
$server->register(	'insereTag',							//method name
	array('tag'=>'xsd:string'),									//input
	array('return'=>'xsd:boolean'),								//output
	'urn:tags',									//namespace
	'urn:tags#insereTag',						//soapaction
	'rpc',														//style
	'encoded',													//use
	'insere uma tag'								//documentation
);

//registro do metodo
$server->register(	'apagaTag',							//method name
	array('tag'=>'xsd:string'),									//input
	array('return'=>'xsd:boolean'),								//output
	'urn:tags',									//namespace
	'urn:tags#apagaTag',						//soapaction
	'rpc',														//style
	'encoded',													//use
	'apaga uma tag'								//documentation
);


//definir metodo 
function listaTags()
{
	//inclusão do arquivo de classes PDO
	require_once('lib/pdo.php');
	$sql ="SELECT * FROM [TheLegend].[dbo].[Tags]";
	$query = $DBH->prepare($sql);
    $query->execute();
	$res = $query->fetchAll(PDO::FETCH_ASSOC);
	if(count($res) >0 ) {
		return $res;
	} else {
		return new soap_fault('Server', '', 'no results to show','');
	}

}

//definir metodo 
function insereTag($tag)
{
	if (empty($tag)) {
		return new soap_fault('Server', '', 'defina o texto da tag','');
	}
	require_once('lib/pdo.php');
	$sql ="INSERT INTO [TheLegend].[dbo].[Tags] VALUES (:tag)";
	$query = $DBH->prepare($sql);
	$query->bindParam(':tag', $tag);
	return $query->execute();
}

//definir metodo 
function apagaTag($tag)
{
	if (empty($tag)) {
		return new soap_fault('Server', '', 'defina o texto da tag','');
	}
	require_once('lib/pdo.php');
	$sql ="DELETE FROM [TheLegend].[dbo].[Tags] WHERE TagName=:tag";
	$query = $DBH->prepare($sql);
	$query->bindParam(':tag', $tag);
	return $query->execute();
}

$HTTP_RAW_POST_DATA=isset($HTTP_RAW_POST_DATA )?$HTTP_RAW_POST_DATA: '';
$server->service($HTTP_RAW_POST_DATA);

?>