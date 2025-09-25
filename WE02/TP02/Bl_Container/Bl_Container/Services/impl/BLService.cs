using Bl_Container.Models;
using Bl_Container.Repository;
using Bl_Container.Services;

namespace Bl_Container.Services
{
    public class BLService : IBLService
    {
        private readonly IBLRepository _repo;

        public BLService(IBLRepository repo)
        {
            _repo = repo;
        }

        public int save(BL dtoBL) => _repo.insert(dtoBL);

        public BL select(BL dtoBL) => _repo.select(dtoBL);

        public List<BL> selectAll() => _repo.selectAll();

        public BL update(BL dtoBL)
        {
            _repo.update(dtoBL);
            return _repo.select(dtoBL);
        }

        public int delete(BL dtoBL) => _repo.delete(dtoBL);
    }
}
