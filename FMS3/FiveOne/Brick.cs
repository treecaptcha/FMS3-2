namespace MonoBrick.FiveOne
{
    /**
	 * Class representing a 51515 Brick
	 */
    public class Brick {
        // readonly == final
        private readonly Motor motorA;
        private readonly Motor motorB;
        private readonly Motor motorC;
        private readonly Motor motorD;
        private readonly Motor motorE;
        private readonly Motor motorF;
        private readonly Connector connector;





        /// <summary>
        /// Motor A
        /// </summary>
        /// <value>
        /// The motor connected to port A
        /// </value>
        public Motor MotorA {
            get { return motorA; }
        }

        /// <summary>
        /// Motor B
        /// </summary>
        /// <value>
        /// The motor connected to port B
        /// </value>
        public Motor MotorB {
            get { return motorB; }
        }

        /// <summary>
        /// Motor C
        /// </summary>
        /// <value>
        /// The motor connected to port C
        /// </value>
        public Motor MotorC {
            get { return motorC; }
        }

        /// <summary>
        /// Motor D
        /// </summary>
        /// <value>
        /// The motor connected to port D
        /// </value>
        public Motor MotorD {
            get { return motorD; }
        }



        /**
		 * Creates an object representing a 51515 brick
		 *
		 * @param port - the serial port that the brick can be found on. On Windows this could be COM5.
		 *
		 * @param autoInject - whether to stop whatever is running on the brick and prepare for motors.
		 *
		 * @param debug - whether to print messages at some points to aid in debugging.
		 * 
		 * @throws IOException - (only applicable when autoInject) if it is unable to stop the brick for whatever reason (this is highly unlikely).
		 *
		 * @throws FileNotFoundException - when the serial port does not exist.
		 *
		 * @throws UnauthorizedAccessException - if another application is holding control of this device.
		 */
        public Brick(string port, bool autoInject = true, bool debug = false) {
            connector = new Connector(port, autoInject, debug);
            motorA = new Motor("A", connector);
            motorB = new Motor("B", connector);
            motorC = new Motor("C", connector);
            motorD = new Motor("D", connector);
            motorE = new Motor("E", connector);
            motorF = new Motor("F", connector);
        } 
		
		
        /**
		 * stop running programs on the brick and prepares for motor control.
		 * 
		 * @throws IOException - if it is unable to stop the brick for whatever reason (this is highly unlikely).
		 */
        public void inject() {
            connector.inject();
        }
		
        /**
		 * stop running programs on the brick.
		 * 
		 * @throws IOException - if it is unable to stop the brick for whatever reason (this is highly unlikely).
		 */
        public void stopPrograms () {
            connector.interrupt();
        }
		
        /**
		 * Write the Read Evaluate Print Loop to the Console.
		 * This may do nothing in future releases!
		 */
        public void logRepl() {
            connector.logRepl();
        }

    }
	
    public class Motor : /* implements */ IMotor{
        private string modar;
        private Connector com;
        /// legder is so the motor knows who they are! legder is always capital!
        public Motor(string legder, Connector con)
        {
            this.modar = "motor" + legder;
            this.com = con;
        }
        public bool Reverse { get; set; }
        public void On(sbyte speed)
        {
            On(speed, false);
        }

        public void On(sbyte speed, bool reply) {
            if (Reverse) {
                com.send(modar + ".run_at_speed(" + (speed * -1) + ")");
            }
            else {
                com.send(modar + ".run_at_speed(" + speed + ")");
            }
        }

        public void Brake() {
            Brake(false);
        }

        public void Brake(bool reply) {
            com.send(modar + ".brake()");
        }

        public void Off()
        {
            Off(false);
        }

        public void Off(bool reply)
        {
            On(0);
        }

        public void ResetTacho()
        {
            // reset tacos?
        }

        public void ResetTacho(bool reply)
        {
            // I'm not sure what a reply is
        }

        public int GetTachoCount()
        {
            // ummm....
            return 0;
        }

        public bool IsRunning() {
            // probably?
            return true;
        }
    }
}