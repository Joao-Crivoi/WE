using Bl_Container.Models;
using Bl_Container.Repository;

namespace Bl_Container.Services
{
    public class ContainerService : IContainerService
    {
        private readonly IContainerRepository _repo;

        public ContainerService(IContainerRepository repo)
        {
            _repo = repo;
        }

        public int save(Container dtoContainer) => _repo.insert(dtoContainer);

        public Container select(Container dtoContainer)
        {
            _repo.select(dtoContainer);
            return dtoContainer;
        }

        public List<Container> selectAll()
        {
            return _repo.selectAll();
            
        }


        public Container update(Container dtoContainer)
        {
            _repo.update(dtoContainer);
            _repo.select(dtoContainer);
            return dtoContainer;
        }

        public int delete(Container dtoContainer) => _repo.delete(dtoContainer);
    }
}
