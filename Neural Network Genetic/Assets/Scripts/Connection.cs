public class Connection
{
    public INeuralNetworkInputNode node;
    public float weight;

    public Connection(INeuralNetworkInputNode node, float weight)
    {
        this.node = node;
        this.weight = weight;
    }
}