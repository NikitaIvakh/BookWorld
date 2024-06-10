using FluentAssertions;
using NetArchTest.Rules;

namespace Coupons.ArchitectureTests;

public sealed class ArchitectureTests
{
    private const string DomainNamespace = "Coupons.Domain";
    private const string ApplicationNamespace = "Coupons.Application";
    private const string InfrastructureNamespace = "Coupons.Infrastructure";
    private const string PersistenceNamespace = "Coupons.Persistence";
    private const string ApiNamespace = "Coupons.API";

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Domain.AssemblyReferences.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PersistenceNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAny(otherProjects).GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Application.DependencyInjection.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            InfrastructureNamespace,
            PersistenceNamespace,
            ApiNamespace,
        };

        // Act
        var testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAny(otherProjects).GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveOtherDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Infrastructure.DependencyInjection.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            PersistenceNamespace,
            ApiNamespace,
        };

        // Act
        var testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAny(otherProjects).GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Persistence_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Persistence.DependencyInjection.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            ApiNamespace
        };

        // Act
        var testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAny(otherProjects).GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handlers_Should_DependencyOnDomain()
    {
        // Arrange
        var assembly = typeof(Application.DependencyInjection.AssemblyReference).Assembly;

        // Act
        var testResult = Types.InAssembly(assembly).That().HaveNameEndingWith("Handler").Should().HaveDependencyOn(DomainNamespace).GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Controllers_Should_HaveDependencyOnMediatR()
    {
        // Arrange
        var assembly = typeof(API.AssemblyReferences.AssemblyReference).Assembly;

        // Act
        var testResults = Types.InAssembly(assembly).That().HaveNameEndingWith("Controller").Should().HaveDependencyOn("MediatR").GetResult();

        // Assert
        testResults.IsSuccessful.Should().BeTrue();
    }
}