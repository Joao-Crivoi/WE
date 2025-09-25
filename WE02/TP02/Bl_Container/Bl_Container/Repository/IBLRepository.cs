namespace Bl_Container.Repository
{
    public interface IBLRepository
    {
        public int insert(Models.BL dtoBl);

        public Models.BL select(Models.BL dtoBl);

        public List<Models.BL> selectAll();

        public int update(Models.BL dtoBl);

        public int delete(Models.BL dtoBl);
        
    }
}
