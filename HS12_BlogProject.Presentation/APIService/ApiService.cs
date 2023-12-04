﻿using System.Net.Http.Headers;
using System.Text;
using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Presentation.Models;
using Newtonsoft.Json;

namespace HS12_BlogProject.Presentation.APIService
{
	public class ApiService : IApiService
	{
		private readonly HttpClient _httpClient;

		public ApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<T> GetAsync<T>(string endpoint, string token)
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _httpClient.GetAsync(endpoint);

			if (!response.IsSuccessStatusCode)
			{

				throw new Exception($"API isteği başarısız: {response.StatusCode}");
			}

			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(content);
		}

		public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data, string token)
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var jsonData = JsonConvert.SerializeObject(data);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(endpoint, content);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"API isteği başarısız: {response.StatusCode}");
			}

			var responseContent = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TResponse>(responseContent);
		}

		//getbyid
		public async Task<T> GetByIdAsync<T>(string endpoint, int id, string token)
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.GetAsync($"{endpoint}/{id}");

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"API isteği başarısız: {response.StatusCode}");
			}

			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(content);
		}

		//delete
		public async Task<T> DeleteAsync<T>(string endpoint, int id, string token)
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.DeleteAsync($"{endpoint}/{id}");

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"API isteği başarısız: {response.StatusCode}");
			}

			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(content);
		}

		//update
		public async Task<T> UpdateAsync<T>(string endpoint, T data, string token)
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var jsonData = JsonConvert.SerializeObject(data);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync(endpoint, content);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"API isteği başarısız: {response.StatusCode}");
			}

			var responseContent = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(responseContent);
		}

		//get created model
		public async Task<T> GetCreateModelAsync<T>(string endpoint, string token)
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.GetAsync($"{endpoint}");

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"API isteği başarısız: {response.StatusCode}");
			}
			var responseContent = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(responseContent);
		}

		// login
		public async Task<TokenResponse> LoginAsync(LoginDTO loginModel)
		{
			var jsonData = JsonConvert.SerializeObject(loginModel);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("account/login", content);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"API isteği başarısız: {response.StatusCode}");
			}

			var responseContent = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TokenResponse>(responseContent);
		}

		//register
		public async Task<bool> RegisterAsync(RegisterDTO registerModel)
		{
			var jsonData = JsonConvert.SerializeObject(registerModel);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("account/register", content);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"API isteği başarısız: {response.StatusCode}");
			}

			return true;
		}
	}
}
