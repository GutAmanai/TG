function inicio() {
    document.addEventListener("deviceready", onDeviceReady, false);
    // correcao tamanho das imagens          
    // --------------------------------------------------------------------------------
    // --------------------------------------------------------------------------------  
    $('.logo-url-empresa').width(40);
    $('.logo-empresa').width(70);
    $('.logo-lotus').width(47);	 
}

function onDeviceReady() {
    //lerArquivoTxt(); 
}

function geraLista(data){
    
	for(var i in data.promocoes){
	    $('.lista-promocoes').append('<li class="well">' +
		'<div class="container corpo-promocoes">'+
			'<!--Barra titulo empresa -->' +
			'<div class="titulo-empresa navbar-inner" id="titulo-empresa">'+
				//imagem da empresa da promocao
			'	<img src="'+data.promocoes[i].UrlEmpresa+'"'+
				' class="pull-left logo-promocao" id="id-url-empresa">'+
				'<div class="container">'+
				//nome da empresa
			'		<a class="brand" href="#">'+data.promocoes[i].NomeEmpresa+'</a>'+
			'	</div>'+
			'</div>'+
			'<!--Barra titulo empresa -->'+
			'<!--Imagem da promocao -->'+
			'<div class="imagem-promocao thumbnail" id="'+data.promocoes[i].IdEmpresa + '">' +
			//imagem da promocao
			'	<img class="imagem" src="'+data.promocoes[i].UrlPromocao+'"'+
			'	width="" height="">'+
			'</div>'+
			'<!--Imagem da promocao -->'+
			'<div class="control-group">'+
				'<div class="controls">'+
					'<label>'+
					//texto da promocao			
					data.promocoes[i].Promocao+
					'</label>'+					
				'</div>'+
			'</div>'+
		'</div>'+
	'</li>');
	}
}

function lerArquivoTxt() {
    window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, gotFS, fail); 
}

function onFileSuccess(fileSystem) {
    console.log(fileSystem.name);
}

function onResolveSuccess(fileEntry) {
    console.log(fileEntry.name);
}

function gotFS(fileSystem) {
    fileSystem.root.getFile("/app/www/data.txt", null, gotFileEntry, fail);
}

function gotFileEntry(fileEntry) {
    fileEntry.file(gotFile, fail);
}

function gotFile(file) {
    //readDataUrl(file);
    readAsText(file);
}

function readDataUrl(file) {
    var reader = new FileReader();
    reader.onloadend = function (evt) {
        console.log("Read as data URL");
        console.log(evt.target.result);
    };
    reader.readAsDataURL(file);
}

function readAsText(file) {
    var reader = new FileReader();
    reader.onloadend = function (evt) {
        console.log("Read as text");
        console.log(evt.target.result);
        //chamada para gerar o html
        geraLista(evt.target.result);
        localStorage.dataJSON = evt.target.result;
    };
    reader.readAsText(file);

}

function fail(evt) {
    console.log(evt.target.error.code);
}



