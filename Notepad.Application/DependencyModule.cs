using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notepad.Application.Configs;
using Notepad.Application.Context;
using Notepad.Application.Features.NoteFeatures.Commands;
using Notepad.Application.Features.NoteFeatures.Queries;
using Notepad.Application.Features.NoteFeatures.Validators;
using Notepad.Application.Features.TagFeatures.Validators;
using Notepad.Application.Interfaces;
using System.Reflection;

namespace Notepad.Application
{
    public static class DependencyModule
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.RegisterValidators();
        }

        private static void RegisterValidators(this IServiceCollection services)
        {
            //Note validators
            services.AddTransient<IValidator<CreateNoteCommand>, CreateNoteCommandValidator>();
            services.AddTransient<IValidator<DeleteNoteByIdCommand>, DeleteNoteCommandValidator>();
            services.AddTransient<IValidator<UpdateNoteCommand>, UpdateNoteCommandValidator>();
            services.AddTransient<IValidator<SearchNotesQuery>, SearchNotesQueryValidator>();

            //Tag validators
            services.AddTransient<IValidator<CreateTagCommand>, CreateTagCommandValidator>();
            services.AddTransient<IValidator<DeleteTagByIdCommand>, DeleteTagCommandValidator>();
            services.AddTransient<IValidator<UpdateTagCommand>, UpdateTagCommandValidator>();

            //User context
            services.AddScoped<IUserContext, UserContext>();

            //Configs
            services.AddTransient<AuthConfig>();

        }
    }
}
