<?php 
//inclusão do arquivo de classes NuSOAP
require_once('lib/nusoap.php');

//Criar  instancia 
$server = new soap_server;

$server->configureWSDL('users', 'urn:users');
$server->wsdl->schemaTargetNamespace='urn:users';
//registro do metodo
$server->register(	'autenticaUser',							//method name
	array('email'=>'xsd:string'),								//input
	array('return'=>'xsd:integer'),								//output
	'urn:users',									//namespace
	'urn:users#autenticaUser',						//soapaction
	'rpc',														//style
	'encoded',													//use
	'Retorna id do user se valido'								//documentation
);
//definir metodo 
function autenticaUser($email)
{
	//inclusão do arquivo de classes PDO
	require_once('lib/pdo.php');
	//$email='aaa@bbb.ccc';
	$sql ="SELECT [UserId] FROM [TheLegend].[dbo].[UserProfile] WHERE Email=:email";
	$query = $DBH->prepare($sql);
	$query->bindParam(':email', $email);
    $query->execute();
    $row = $query->fetch(PDO::FETCH_ASSOC);
	//print_r($row);
	if(count ($row) == 1 ) {
		return $row["UserId"];
	} else {
		return 0;
	}
	return new soap_fault('Server', '', 'no results to show','');
}

$server->register(	'totalUsers',							//method name
	array(),								//input
	array('return'=>'xsd:integer'),								//output
	'urn:users',									//namespace
	'urn:users#totalUsers',						//soapaction
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

$server->wsdl->addComplexType(
    'userArray',
    'complexType',
    'struct',
    'all',
    '',
    array(
        'UserName' => array('name' => 'UserName', 'type' => 'xsd:string'),
        'Birth' => array('name' => 'Birth', 'type' => 'xsd:dateTime'),
        'Sex' => array('name' => 'Sex', 'type' => 'xsd:string'),
		'Email' => array('name' => 'Email', 'type' => 'xsd:string'),
		'HumorId' => array('name' => 'HumorId', 'type' => 'xsd:integer')
    )
);
$server->register(	'insereUser',							//method name
	array('user'=>'tns:userArray'),								//input
	array('return'=>'xsd:integer'),								//output
	'urn:users',									//namespace
	'urn:users#totalUsers',						//soapaction
	'rpc',														//style
	'encoded',													//use
	'insere um novo user'								//documentation
);

//definir metodo 
function insereUser($user)
{
	if (!is_array($user)) {
		return new soap_fault('Server', '', 'verifique os dadso a inserir','');
	}
	require_once('lib/pdo.php');
	$sql ="INSERT INTO TheLegend.dbo.UserProfile VALUES (:username, :birth, :sex, :email, :humorid)";
	$query = $DBH->prepare($sql);
	$query->bindParam(':username', $user['UserName']);
	$query->bindParam(':birth', $user['Birth']);
	$query->bindParam(':sex', $user['Sex']);
	$query->bindParam(':email', $user['Email']);
	$query->bindParam(':humorid', $user['HumorId']);
	return $query->execute();
}

$HTTP_RAW_POST_DATA=isset($HTTP_RAW_POST_DATA )?$HTTP_RAW_POST_DATA: '';
$server->service($HTTP_RAW_POST_DATA);

?>