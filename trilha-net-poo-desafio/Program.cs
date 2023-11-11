using DesafioPOO.Models;

try
{
    var celNokia = new DesafioPOO.Models.Nokia("11976767676","Nokia01","89754893707890354278904352",64);
    celNokia.Ligar();
    celNokia.InstalarAplicativo("MeuApp");

    var Iphone = new DesafioPOO.Models.Iphone("1197834287","IPhone01","87943789354283542578935427",128);
    Iphone.ReceberLigacao();
    Iphone.InstalarAplicativo("MaisQueUauApp");
    
}
catch (System.Exception)
{
    throw;
}