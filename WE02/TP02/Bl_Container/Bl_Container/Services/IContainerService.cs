namespace Bl_Container.Services
{
    public interface IContainerService
    {
        public int save(Models.Container dtoContainer);
        public Models.Container select(Models.Container dtoContainer);
        public List<Models.Container> selectAll();
        public Models.Container update(Models.Container dtoContainer);
        public int delete(Models.Container dtoContainer);

    }
}
