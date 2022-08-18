
using Api.Behaviours;
using Api.Middlewares;
using ApplicationServices.Customer.Commands.CreateCustomer;
using ApplicationServices.Customer.Commands.DeleteCustomer;
using ApplicationServices.Customer.Commands.EditCustomer;
using ApplicationServices.Customer.Queries.GetCustomerById;
using ApplicationServices.Mapper;
using DataLayer.SqlServer.Common;
using DataLayer.SqlServer.EventStore;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddFluentValidation();

builder.Services.AddTransient<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();
builder.Services.AddTransient<IValidator<EditCustomerCommand>, EditCustomerCommandValidator>();
builder.Services.AddTransient<IValidator<DeleteCustomerCommand>, DeleteCustomerCommandValidator>();

builder.Services.AddMediatR(typeof(CreateCustomerCommandHandler));
builder.Services.AddMediatR(typeof(GetCustomerByIdQueryHandler));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cnnString = builder.Configuration.GetConnectionString("AppCnn");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cnnString));
builder.Services.AddAutoMapper(typeof(DomainProfile).Assembly);

builder.Services.AddSingleton<IEventStoreDbContext, EventStoreDbContext>();

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EventLoggerBehavior<,>));



var app = builder.Build();

// Make Sure Database Created
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
    var context = serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context?.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
