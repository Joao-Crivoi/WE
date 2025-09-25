namespace Bl_Container.Models
{
    public class BL
    {
        public int ID { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string Consignee { get; set; } = string.Empty;
        public String Navio { get; set; }


        public BL() { }

        public BL(int id, string numero, string consignee, string navio)
        {
            ID = id;
            Numero = numero;
            Consignee = consignee;
            Navio = navio;
        }
    }
}
