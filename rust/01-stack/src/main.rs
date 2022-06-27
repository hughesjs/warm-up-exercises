fn main() {
    let mut stack = Stack::new();
    stack.push(42);
    stack.push(555);
    stack.push(666);

    // Check I can push and pop everything correctly
    assert!(stack.pop().unwrap() == 666);
    assert!(stack.pop().unwrap() == 555);
    assert!(stack.pop().unwrap() == 42);

    // Check it returns none when the stack is empty (no exceptions in rust)
    assert!(stack.pop().is_none());
}


struct Stack<T> {
    stack: Vec<T>
}

impl<T> Stack<T> {
    fn new() -> Self {
        Stack {stack: Vec::new()}
    }

    fn pop(&mut self) -> Option<T> {
        return self.stack.pop();
    }

    fn push(&mut self, item: T) {
        self.stack.push(item);
    }
}

