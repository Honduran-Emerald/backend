using Emerald.Application.Models.Quest.Component;
using Emerald.Domain.Models.ComponentAggregate;
using System;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class ComponentModelFactory : IModelFactory<Component, ComponentModel>
    {
        public async Task<ComponentModel> Create(Component component)
        {
            switch (component)
            {
                case TextComponent textComponent:
                    return new TextComponentModel(
                        component.Id.ToString(),
                        textComponent.Text);

                case ImageComponent imageComponent:
                    return new ImageComponentModel(
                        component.Id.ToString(),
                        imageComponent.ImageId);

                case AnswerComponent answerComponent:
                    return new AnswerComponentModel(
                        component.Id.ToString(),
                        answerComponent.Text);

                default:
                    throw new Exception("Got invalid component type");
            }
        }
    }
}
