namespace FlextStarTubeApi.Model
{
    public class Criador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }


    }

}
