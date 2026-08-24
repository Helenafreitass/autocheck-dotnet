using System;

namespace AutoCheck.ConsoleApp.Models
{
    // Representa um item avaliado na vistoria (ex: "Nível de Óleo do Motor" -> "Bom")
    public class ItemVistoria
    {
        public string Nome { get; set; }
        public string Status { get; set; }

        public ItemVistoria(string nome, string status)
        {
            this.Nome = nome;

            // só aceito os 3 status que o projeto pede
            if (status == "Bom" || status == "Regular" || status == "Ruim")
            {
                this.Status = status;
            }
            else
            {
                // se vier algo digitado errado eu jogo pra "Regular" só pra não travar o programa
                Console.WriteLine("Status invalido, assumindo 'Regular' para o item " + nome);
                this.Status = "Regular";
            }
        }
    }
}
