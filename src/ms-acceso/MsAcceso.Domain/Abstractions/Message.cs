
namespace MsAcceso.Domain.Abstractions;

public class Message
{

    public static string Create = "Se registro correctamente";  
    public static string Update = "Se actualizo correctamente";  
    public static string Delete = "Se elimino correctamente";  
    public static string Desactivate = "Se desactivo correctamente";  
    public static string Desactivate2(bool Value){
        if(Value){
            return "Se activo correctamente";
        }else{
            return "Se desactivo correctamente";
        }
    } 
}