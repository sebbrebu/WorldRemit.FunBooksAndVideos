# WorldRemit.FunBooksAndVideos

For the business requirement, please see DotNetTest.docx

## Flexible Rule Engine

In order for business rules to be easily added to the purchase order processor, `OrderService` takes a list of `IBusinessRule` as constructor parameter.

A new business rule can be created by implementing the `ApplyAsync` method of the `IBusinessRule` interface.

A new business rule can be added to the purchase order processor by configuring the dependency injection section in `ConfigureServices` in `Startup` like so:
```
services.AddSingleton<IOrderService>(s =>
                new OrderService(new List<IBusinessRule>()
                    {
                        s.GetService<ActivateMembershipRule>(),
                        s.GetService<ProductShippingSlipRule>()
                        //Add further IBusinessRule implementations here
                    })
                );
```
