using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Model.DS
{
    

    // A class to represent a queue The queue,
    // front stores the front node of LL and
    // rear stores the last node of LL
    class Queue<T>
    {
        private class QNode
        {
            public T key;
            public QNode next;

            // constructor to create
            // a new linked list node
            public QNode(T key)
            {
                this.key = key;
                this.next = null;
            }
        }

        QNode front, rear;
        T flagFailed;
        public Queue(T flagFailed)
        {
            this.front = this.rear = null;
            this.flagFailed = flagFailed;
        }

        // Method to add an key to the queue.
        public void enqueue(T key)
        {

            // Create a new LL node
            QNode temp = new QNode(key);

            // If queue is empty, then new
            // node is front and rear both
            if (this.rear == null)
            {
                this.front = this.rear = temp;
                return;
            }

            // Add the new node at the
            // end of queue and change rear
            this.rear.next = temp;
            this.rear = temp;
        }

        // Method to remove an key from queue.
        public void dequeue()
        {
            // If queue is empty, return NULL.
            if (this.front == null)
                return;

            // Store previous front and
            // move front one node ahead
            QNode temp = this.front;
            this.front = this.front.next;

            // If front becomes NULL,
            // then change rear also as NULL
            if (this.front == null)
                this.rear = null;
        }

        public T peekFront()
        {
            if (this.front!=null)
            {
                return this.front.key;
            }
            else
            {
                return flagFailed;
            }
        }
        public T peekRear()
        { 
            if (this.rear != null)
            {
                return this.rear.key;
            }
            else
            {
                return flagFailed;
            }
        }
        public void display()
        {
            // check for stack underflow
            if (this.front == null)
            {
                Console.Write("\nQueue Empty");
                return;
            }
            else
            {
                QNode temp = this.front;
                while (temp != null)
                {

                    // print node data
                    Console.Write("{0}->", temp.key);

                    // assign temp link to temp
                    temp = temp.next;
                }
                Console.WriteLine("");
            }
        }
    }
}
