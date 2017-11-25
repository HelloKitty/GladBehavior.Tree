# GladBehavior.Tree

GladBehavior.Tree is a Behavior Tree (BT) implementation that supports generic contexts/agents. There are many differing implementations of behavior trees but GladBehavior.Tree attempts to adhere to the common implementation found in many other popular libraries. Behavior Trees allow for modular and reusable construction of AI models. With the custom nodes being the reusable components that compose a Behavior Tree.

## Node Types

### Selector

The selector node in GladBehavior.Tree is the OR of the nodes meaning that evaluation of composed nodes is short-circuited. This means nodes are evaluated until a Success, Running state, or all of them fail. This node re-enters Running nodes during subsequent node evaluation.

### Sequence

The sequence node in GladBehavior.Tree is the AND of the nodes meaning that evaluation of composed nodes succeeds only if all nodes succeed. This means nodes are evaluated until all are Success, a Running state is encountered, or one of them fails. This node re-enters Running nodes during subsequent node evaluation.

### Inverter

The inverter node in GladBehavior.Tree is a decorator that decorates a node, which can be considered a child node, and inverts the resulting evaluated state. Success is transformed to Failure and vice-versa. Running state is left unchanged.

### Condition

The condition node in GladBehavior.Tree is a leaf that provides a simplified binary API for evaluating a condition. A condition is either true of false meaning Success and Failure respectively. A condition node can never be in a Running state.

### PrioritySelector

The priority selector node in GladBehavior.Tree is exaclty like the Selector node however it emphasizes priority nodes, nodes first in the child composite collection, by re-running from the begining every evaluation regardless of a potentially running node.

### PrioritySequence

The priority sequence node in GladBehavior.Tree is exaclty like the Sequence node however it emphasizes priority nodes, nodes first in the child composite collection, by re-running from the begining every evaluation regardless of a potentially running node.

## Tests

TODO

## Builds

**NuGet:** TODO
**MyGet:** TODO

## License

MIT
