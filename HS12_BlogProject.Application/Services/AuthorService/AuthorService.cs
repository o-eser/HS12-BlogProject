using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Common.Models.VMs;
using HS12_BlogProject.Domain.Entities;
using HS12_BlogProject.Domain.Enums;
using HS12_BlogProject.Domain.Repositories;

namespace HS12_BlogProject.Application.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task Create(CreateAuthorDTO model)
        {
            Author author = new Author()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImagePath = model.ImagePath,
            };

            //Todo: Mapping

            if (model.UploadPath != null)
            {
                //Sixlabors.ImageSharp
                Image image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(200, 200));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");

                author.ImagePath = $"/images/{guid}.jpg";
            }
            else
                author.ImagePath = "/images/defaultauthor.jpg";

            await _authorRepository.Create(author);
        }



        public async Task Delete(int id)
        {
            Author author = await _authorRepository.GetDefault(g => g.Id == id);

            if (id == 0)
            {
                throw new ArgumentException("Id 0 Olamaz!");

            }
            else if (author == null)
            {
                throw new ArgumentException("Böyle bir yazar mevcut değil!");
            }

            author.Status = Status.Passive;
            author.DeletedDate = DateTime.Now;

            await _authorRepository.Delete(author);
        }

        public async Task<UpdateAuthorDTO> GetById(int id)
        {
            return await _authorRepository.GetFilteredFirstOrDefault(x => new UpdateAuthorDTO
            {
                Id = id,
                FirstName = x.FirstName,
                LastName = x.LastName,
            }, g => g.Id == id && g.Status != Status.Passive);
        }

        public async Task<List<AuthorVM>> GetAuthors()
        {
            ICollection<AuthorVM> posts = await _authorRepository.GetFilteredList(x => new AuthorVM
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Id = x.Id,

            }, g => g.Status != Status.Passive,
            x => x.OrderBy(x => x.FirstName).ThenBy(x => x.LastName));

            return posts.ToList();
        }

        public async Task Update(UpdateAuthorDTO model)
        {
            Author author = new Author()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImagePath = model.ImagePath,
                Status = model.Status,
                UpdateDate = model.UpdateDate,
            };


            if (model.UploadPath != null)
            {
                //Sixlabors.ImageSharp
                Image image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(200, 200));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");

                author.ImagePath = $"/images/{guid}.jpg";
            }
            else
                author.ImagePath = "/images/defaultauthor.jpg";

            await _authorRepository.Update(author);
        }

        public async Task<CreateAuthorDTO> CreateAuthor()
        {
            return new CreateAuthorDTO();
        }
    }
}
