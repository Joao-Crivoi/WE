using Bl_Container.Models;

namespace Bl_Container.Repository
{
    public interface IContainerRepository
    {
        public int insert(Models.Container dtoContainer);

        public Container select(Container dtoContainer);

        public List<Models.Container> selectAll();

        public int update(Models.Container dtoContainer);

        public int delete(Models.Container dtoContainer);
    }
}
