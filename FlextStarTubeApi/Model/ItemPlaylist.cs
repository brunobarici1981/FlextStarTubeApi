namespace FlextStarTubeApi.Model
{
    public class ItemPlaylist
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int ConteudoId { get; set; }

        public Playlist Playlist { get; set; }
        public Conteudo Conteudo { get; set; }
    }

}
