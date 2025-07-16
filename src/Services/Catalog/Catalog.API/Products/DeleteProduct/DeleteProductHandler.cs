
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct;


public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductCommandResult>;

public record DeleteProductCommandResult(bool IsSuccess);


public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required.");
    }
}


internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommand> logger) : ICommandHandler<DeleteProductCommand, DeleteProductCommandResult>
{
    public async Task<DeleteProductCommandResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Query}", command);

        session.Delete<Product>(command.Id);
        
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductCommandResult(true);
    }
}
