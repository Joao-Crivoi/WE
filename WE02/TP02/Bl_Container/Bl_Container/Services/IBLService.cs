namespace Bl_Container.Services
{
    public interface IBLService
    {
        public int save(Models.BL dtoBL);
        public List<Models.BL> selectAll();
        public Models.BL select(Models.BL dtoBL);
        public Models.BL update(Models.BL dtoBL);
        public int delete(Models.BL dtoBL);
    }
}
