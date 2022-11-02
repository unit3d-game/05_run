public class MessagePayload
{

    // 事件名称
    public readonly string name;
    // 事件发送者
    public readonly object sender;
    // 数据
    public readonly object data;

    public MessagePayload(string name, object sender, object data)
    {
        this.name = name;
        this.sender = sender;
        this.data = data;
    }
}

