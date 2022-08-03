using Moq;
using StockMarket.Chat.Models;
using StockMarket.Chat.Services;
using StockMarket.Chat.Services.Interfaces;

namespace StockMarket.ChatService.Tests;

public class ChatMessageProcessorTests
{

    private ChatMessageProcessor chatMessageProcessor;
    private Mock<IRabbitStockMsgProducerService> rabbitStockMsgProducerServiceMock;

    [SetUp]
    public void Setup()
    {
        rabbitStockMsgProducerServiceMock = new Mock<IRabbitStockMsgProducerService>();
        chatMessageProcessor = new ChatMessageProcessor(rabbitStockMsgProducerServiceMock.Object);
    }

    [Test]
    public void MessageWithCommandAtStart_ReturnsCommand()
    {
        var command = chatMessageProcessor.getCommandFromMessage("/stock=asas");

        Assert.That(command, Is.EqualTo("stock"));
    }

    [Test]
    public void MessageWithCommandInTheMiddle_ReturnsEmpty()
    {
        var command = chatMessageProcessor.getCommandFromMessage("a/stock=asas");

        Assert.IsEmpty(command);
    }

    [Test]
    public void MessageWithCommandThatContainsLowercaseAndUppercase_ReturnsCommand()
    {
        var command = chatMessageProcessor.getCommandFromMessage("/StOcK=asas");

        Assert.That(command, Is.EqualTo("stock"));
    }

    [Test]
    public void MessageWithCommandThatContainsNumbers_ReturnsEmpty()
    {
        var command = chatMessageProcessor.getCommandFromMessage("/St1OcK=asas");

        Assert.IsEmpty(command);
    }

    [Test]
    public void MessageWithCommandThatContainsNumbers_InvalidCommand()
    {
        var value = chatMessageProcessor.getValueForCommand("stock", "/StocK=asas");

        Assert.That(value, Is.EqualTo("asas"));
    }

    [Test]
    public void RabbitMsgProducerCalledWithParameters_ExecutedOneTime()
    {
        // arrange
        string message = "/stock=aapl.us";
        string room = "Room1";

        RabbitStockMsg stockMsg = new RabbitStockMsg
        {
            Message = "aapl.us",
            Room = "Room1"
        };

        // act
        chatMessageProcessor.ProccessChatMessage(message, room);

        // assert
        rabbitStockMsgProducerServiceMock.Verify(s => s.SendStockMessage(stockMsg), Times.Once);
    }
}
