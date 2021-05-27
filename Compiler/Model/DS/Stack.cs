using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Model.DS
{
    class Stack <T>
    {
        // A linked list node
        private class Node
        {
            // integer data
            public T data;

            // reference variable Node type
            public Node link;
        }

        // create global top reference variable
        Node top;
        T flagFailed;
        // Constructor
        // flag failed is the message will return in failed operation
        public Stack(T flagFailed)
        {
            this.top = null;
            this.flagFailed = flagFailed;
        }

        // Utility function to add
        // an element x in the stack
        // insert at the beginning
        public void push(T x)
        {
            // create new node temp and allocate memory
            Node temp = new Node();

            // check if stack (heap) is full.
            // Then inserting an element
            // would lead to stack overflow
            if (temp == null)
            {
                Console.Write("\nHeap Overflow");
                return;
            }

            // initialize data into temp data field
            temp.data = x;

            // put top reference into temp link
            temp.link = top;

            // update top reference
            top = temp;
        }

        // Utility function to check if
        // the stack is empty or not
        public bool isEmpty()
        {
            return top == null;
        }

        // Utility function to return
        // top element in a stack
        public T peek()
        {
            // check for empty stack
            if (!isEmpty())
            {
                return top.data;
            }
            else
            {
                Console.WriteLine("Stack is empty");
                return this.flagFailed;
            }
        }

        // Utility function to pop top element from the stack
        public void pop() // remove at the beginning
        {
            // check for stack underflow
            if (top == null)
            {
                Console.Write("\nStack Underflow");
                return;
            }

            // update the top pointer to
            // point to the next node
            top = (top).link;
        }

        public void display()
        {
            // check for stack underflow
            if (top == null)
            {
                Console.Write("\nStack Underflow");
                return;
            }
            else
            {
                Node temp = top;
                while (temp != null)
                {

                    // print node data
                    Console.Write("{0}->", temp.data);

                    // assign temp link to temp
                    temp = temp.link;
                }
            }
        }
    }
}
