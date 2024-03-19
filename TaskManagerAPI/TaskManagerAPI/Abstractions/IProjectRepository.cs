using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Abstractions;

public interface IProjectRepository
{
    Task<Project> Add(Project project);
    
    Task<List<Project>> GetAllProjects();
    
    /// <summary>
    /// получить список проектов, в которых участвует пользователь
    /// </summary>
    Task<List<Project>> GetAllProjectsByUser(String userLogin);
}