using YuNLTDotNetTrainingBatch2.POS;


Console.WriteLine("Welcome from Mini Pos");
Result:
Console.WriteLine("Mini Pos Menu");
Console.WriteLine("------------------------------------------------");
Console.WriteLine("1.Product");
Console.WriteLine("2.Sale");
Console.WriteLine("3.Exit");
Console.WriteLine("------------------------------------------------");

Console.WriteLine("Choose Menu");
var input = Console.ReadLine()!;
bool isInt = int.TryParse(input, out int no);
if (!isInt)
{
    Console.WriteLine("Invalid Product Menu. Please choose 1 to 3");
    goto Result;
}
ProductUI productUI = new ProductUI();
SaleUI saleUI = new SaleUI();
EnumPosMenue posmenu = (EnumPosMenue)no;
switch (posmenu)
{
    case EnumPosMenue.Product:
        productUI.Execute();
        goto Result;
    case EnumPosMenue.Sale:
        saleUI.Execute();
        goto Result;
    case EnumPosMenue.Exit:
        goto End;
    case EnumPosMenue.None:
       default:
        Console.WriteLine("Invalid choice. Please enter 1 to 4");
        goto Result;
}
End:
Console.WriteLine("Exiting Mini Pos ....................................");