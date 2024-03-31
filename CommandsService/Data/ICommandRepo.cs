using CommandsService.Models;

namespace CommandsService.Data{
    public interface ICommandRepo{
        
        bool SaveChanges();
        //Platform related stuff
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform plat);
        bool PlatformExists(int platformId);

        //Commands related stuff
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);

        void CreateCommand(int platformId, Command command);

    }
}