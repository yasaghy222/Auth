using Authenticate.Common;
using Authenticate.Context;
using Authenticate.DTOs;
using Authenticate.Entities;
using Authenticate.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("[controller]")]
    [ApiController]
    public class OrganizationController(AuthenticateContextDb db, IValidator<OrganizationDto> validator) : ControllerBase
    {
        private AuthenticateContextDb Db { get; } = db;
        private readonly IValidator<OrganizationDto> OrganizationValidator = validator;

        [HttpGet]
        public IActionResult Get()
        {
            Result result = new Result();

            try
            {
                List<Organization>? list = [.. Db.Organizations];
                result = CustomResults.OrganizationFounded(list);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                //FIXME: Add Logger
                result = CustomErrors.OrganizationQueryFailed();
                return StatusCode(result.StatusCode, result);
            }
        }


        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Result result = new();

            try
            {

                if (string.IsNullOrEmpty(id))
                {
                    result = CustomErrors.OrganizationNotFound();
                    return StatusCode(result.StatusCode, result);
                }

                Organization? founded = Db.Organizations.FirstOrDefault(i => i.Id == new Guid(id));
                if (founded is null)
                {
                    result = CustomErrors.OrganizationNotFound();
                    return StatusCode(result.StatusCode, result);
                }

                result = CustomResults.OrganizationFounded(founded);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                result = CustomErrors.OrganizationQueryFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] string name)
        {
            Result result = new();

            try
            {
                if (string.IsNullOrEmpty(name) && name.Length > 3 && name.Length < 30)
                {
                    result = CustomErrors.InvalidOrganizationData("name is invalid");
                    return StatusCode(result.StatusCode, result);
                }

                Organization org = new()
                {
                    Name = name
                };
                Db.Organizations.Add(org);
                await Db.SaveChangesAsync();

                result = CustomResults.OrganizationAdd(org.Id);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                result = CustomErrors.OrganizationAddFailed();
                return StatusCode(result.StatusCode, result);
            }
        }


        [HttpPatch("/Name/{id}/{name}")]
        public async Task<IActionResult> Edit(string id, string name)
        {
            Result result = new Result();
            OrganizationDto dto = new()
            {
                Id = id,
                Name = name
            };

            try
            {
                var check = OrganizationValidator.Validate(dto);
                if (!check.IsValid)
                {
                    result = CustomErrors.InvalidUserData(check.Errors);
                    return StatusCode(result.StatusCode, result);
                }

                Organization? founded = Db.Organizations.FirstOrDefault(i => i.Id == new Guid(dto.Id));
                if (founded is null)
                {
                    result = CustomErrors.OrganizationNotFound();
                    return StatusCode(result.StatusCode, result);
                }

                founded.Name = dto.Name;
                Db.Organizations.Update(founded);
                await Db.SaveChangesAsync();

                result = CustomResults.OrganizationUpdated();
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                result = CustomErrors.OrganizationUpdateFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

    }
}
