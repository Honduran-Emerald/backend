﻿using Emerald.Domain.Models.ComponentAggregate;
using System;
using System.Collections.Generic;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Components
{
    public class ImageComponentPrototype : ComponentPrototype
    {
        public int? ImageReference { get; set; }

        private ImageComponentPrototype()
        {
        }

        public override Component ConvertToComponent(IPrototypeContext context)
            => new ImageComponent(context.ConvertImageId((int) ImageReference!));

        public override void Verify()
        {
            if (ImageReference == null)
            {
                throw new DomainException($"ImageReference in ImageComponent can not null");
            }
        }

        public override void AggregateImageReferences(List<int> imageReferences)
        {
            if (ImageReference != null)
                imageReferences.Add(ImageReference.Value);
        }
    }
}
