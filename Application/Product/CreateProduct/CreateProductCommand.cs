﻿using MediatR;

namespace Application.Product.CreateProduct
{
    public class CreateProductCommand : ProductDto, IRequest<Unit>
    {

    }
}