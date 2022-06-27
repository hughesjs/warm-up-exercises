fn main() {
    println!("Hello, world!");
}


struct Stack<T> {
    stack: Vec<T>
}

impl<T> Stack<T> {
    fn new() -> Self {
        Stack {stack: Vec::new()}
    }
}