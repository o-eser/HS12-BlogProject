using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Presentation.Models;

namespace HS12_BlogProject.Presentation.APIService
{
	public interface IApiService
	{
		Task<T> GetAsync<T>(string endpoint, string token);
		Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data, string token);

		//getbyid
		Task<T> GetByIdAsync<T>(string endpoint, int id, string token);

		//delete
		Task<T> DeleteAsync<T>(string endpoint, int id, string token);

		//update
		Task<T> UpdateAsync<T>(string endpoint, T data, string token);

		//get created model
		Task<T> GetCreateModelAsync<T>(string endpoint, string token);
		// login
		Task<TokenResponse> LoginAsync(LoginDTO loginModel);

		//register
		Task<bool> RegisterAsync(RegisterDTO registerModel);
	}
}
