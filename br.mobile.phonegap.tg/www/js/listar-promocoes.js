function inicio(){

var ex={ "promocoes":[
			{
				"IdEmpresa" : "1", 
				"NomeEmpresa" : "AOE II",
				"UrlEmpresa" : "http://rocketdock.com/images/screenshots/Age-of-Empires-2.png",
				"UrlPromocao" : "http://upload.wikimedia.org/wikipedia/en/6/6e/Age_of_Empires_II_-_The_Conquerors_Coverart.png",
				"Promocao" : "Age of Empires II: The Conquerors Expansion (sometimes abbreviated to"+
						"AoC or AoK: TC) is the expansion pack to the 1999 real-time strategy game"+
						"Age of Empires II: The Age of Kings."
			},
			{
				"IdEmpresa" : "2", 
				"NomeEmpresa" : "BF 3",
				"UrlEmpresa" : "http://images.wikia.com/battlefield/images/archive/f/f5/20111013142529!Battlefield_3_Icon.png",
				"UrlPromocao" : "http://wallpaperscraft.com/image/battlefield_3_game_name_soldier_army_15725_256x256.jpg",
				"Promocao" : "Battlefield 3 is a first-person shooter video game developed by EA Digital"+
						"Illusions CE and published by Electronic Arts. It is a direct sequel to"+
						"2005's Battlefield 2, and the twelfth installment in the Battlefield franchise."
			}
			]
		 }
	
	geraLista(ex);
	
}

function geraLista(data){
	//comentario
	for(var i in data.promocoes){		
		$('.lista-promocoes').append('<li class="well">' +
		'<div class="container corpo-promocoes">'+
			'<!--Barra titulo empresa -->' +
			'<div class="titulo-empresa navbar-inner" id="titulo-empresa">'+
				//imagem da empresa da promocao
			'	<img src="'+data.promocoes[i].UrlEmpresa+'"'+
				'width="40" height="10" class="pull-left">'+
				'<div class="container">'+
				//nome da empresa
			'		<a class="brand" href="#">'+data.promocoes[i].NomeEmpresa+'</a>'+
			'	</div>'+
			'</div>'+
			'<!--Barra titulo empresa -->'+
			'<!--Imagem da promocao -->'+
			'<div class="imagem-promocao thumbnail" id="imagem-promocao">'+
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
					'<div class="btn-group">'+
					'	<a href="#" class="btn"><i class="icon-thumbs-up"></i></a>'+
					'	<a href="#" class="btn"><i class="icon-thumbs-down"></i></a>'+
					'</div>'+
				'</div>'+
			'</div>'+
		'</div>'+
	'</li>');
	}
}

function geraLista2(data) {
    for (var i in data.promocoes) {
        $('.lista-promocoes').append('<li class="well">' +
   ' <ul class="nav nav-list">' +
    	'<!--Inicio dos itens -->' +
    	'<li class="well">' +
    		'<div class="container">' +
    			'<!--Barra titulo empresa -->' +
    			'<div class="titulo-empresa" id="titulo-empresa">' +
    				'<div class="navbar brand">' +
    					'<div class="navbar-inner">' +
    						'<img src="' + data.promocoes[i].UrlEmpresa + '"' +
    						'<div class="container">' +
    							'<a class="brand" href="#">' + data.promocoes[i].NomeEmpresa + '</a>' +
    						'</div>' +
    					'</div>' +
    				'</div>' +
    			'</div>' +
    			'<!--Barra titulo empresa -->' +
    			'<!--Imagem da promocao -->' +
    			'<div class="imagem-promocao thumbnail" id="imagem-promocao">' +
    				'<img class="imagem" src="' + data.promocoes[i].UrlPromocao + '"' +
    			'</div>' +
    			'<!--Imagem da promocao -->' +
    			'<div class="control-group">' +
    				'<div class="controls">' +
    					'<label>' +
        //texto da promocao			
					data.promocoes[i].Promocao +
					'</label>' +
    					    '<div class="btn-group">' +
    						'<a href="#" class="btn"><i class="icon-thumbs-up"></i></a>' +
    						'<a href="#" class="btn"><i class="icon-thumbs-down"></i></a>' +
    					'</div>' +
    				'</div>' +
    			'</div>' +
    		'</div>' +
    	'</li>');
    }
}

function lerArquivo(){
	return $.getJSON("data.json").pipe(function (data) {
        return data;
    });
}



