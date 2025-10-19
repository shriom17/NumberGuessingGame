namespace NumberGussingGame
{
    public partial class labelTitle : Form
    {
        private int secretNumber;
        private int attempts;

        public labelTitle()
        {
            InitializeComponent();
            StartNewGame();
            CustomizeUI();
        }

        private void CustomizeUI()
        {
            // Add rounded corners effect and additional styling
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Add placeholder text behavior
            txtGuess.Enter += TxtGuess_Enter;
            txtGuess.Leave += TxtGuess_Leave;
            SetPlaceholder();
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrEmpty(txtGuess.Text))
            {
                txtGuess.Text = "Enter your guess...";
                txtGuess.ForeColor = Color.Gray;
            }
        }

        private void TxtGuess_Enter(object? sender, EventArgs e)
        {
            if (txtGuess.Text == "Enter your guess...")
            {
                txtGuess.Text = "";
                txtGuess.ForeColor = Color.Black;
            }
        }

        private void TxtGuess_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGuess.Text))
            {
                SetPlaceholder();
            }
        }

        private void StartNewGame()
        {
            Random random = new Random();
            secretNumber = random.Next(1, 101);
            attempts = 0;
            labelmsg.Text = "Guess a number between 1 and 100";
            labelmsg.ForeColor = Color.FromArgb(52, 73, 94);
            label2.Text = "Attempts: 0";
            label2.BackColor = Color.FromArgb(52, 152, 219);
            txtGuess.Clear();
            SetPlaceholder();
            txtGuess.Focus();
        }

        private void NumberGussing_Load(object sender, EventArgs e)
        {
            // Form load event - keep empty or add initialization code
        }

        private void ProcessGuess()
        {
            // Clear placeholder if present
            if (txtGuess.Text == "Enter your guess...")
            {
                labelmsg.Text = "Please enter a valid number.";
                labelmsg.ForeColor = Color.FromArgb(231, 76, 60);
                return;
            }

            if (int.TryParse(txtGuess.Text, out int userGuess))
            {
                if (userGuess < 1 || userGuess > 100)
                {
                    labelmsg.Text = "Please enter a number between 1 and 100.";
                    labelmsg.ForeColor = Color.FromArgb(231, 76, 60);
                    AnimateControl(labelmsg);
                    return;
                }

                attempts++;
                label2.Text = $"Attempts: {attempts}";

                if (userGuess < secretNumber)
                {
                    labelmsg.Text = "📉 Too low! Try a higher number.";
                    labelmsg.ForeColor = Color.FromArgb(230, 126, 34);
                    label2.BackColor = Color.FromArgb(230, 126, 34);
                    AnimateControl(labelmsg);
                }
                else if (userGuess > secretNumber)
                {
                    labelmsg.Text = "📈 Too high! Try a lower number.";
                    labelmsg.ForeColor = Color.FromArgb(230, 126, 34);
                    label2.BackColor = Color.FromArgb(230, 126, 34);
                    AnimateControl(labelmsg);
                }
                else
                {
                    labelmsg.Text = $"🎉 Congratulations! You found {secretNumber} in {attempts} attempts!";
                    labelmsg.ForeColor = Color.FromArgb(46, 204, 113);
                    label2.BackColor = Color.FromArgb(46, 204, 113);
                    AnimateControl(labelmsg);
                    
                    var result = MessageBox.Show(
                        $"Excellent! You guessed the number {secretNumber} in {attempts} attempts!\n\nWould you like to play again?", 
                        "🏆 Congratulations!", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        StartNewGame();
                    }
                    else
                    {
                        this.Close();
                    }
                }

                txtGuess.Clear();
                SetPlaceholder();
                txtGuess.Focus();
            }
            else
            {
                labelmsg.Text = "Please enter a valid number.";
                labelmsg.ForeColor = Color.FromArgb(231, 76, 60);
                AnimateControl(labelmsg);
                txtGuess.Clear();
                SetPlaceholder();
                txtGuess.Focus();
            }
        }

        private void AnimateControl(Control control)
        {
            // Simple animation effect
            var originalLocation = control.Location;
            control.Location = new Point(originalLocation.X - 3, originalLocation.Y);
            
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            int shakeCount = 0;
            
            timer.Tick += (s, e) =>
            {
                shakeCount++;
                if (shakeCount % 2 == 0)
                    control.Location = new Point(originalLocation.X + 3, originalLocation.Y);
                else
                    control.Location = new Point(originalLocation.X - 3, originalLocation.Y);
                
                if (shakeCount >= 6)
                {
                    control.Location = originalLocation;
                    timer.Stop();
                    timer.Dispose();
                }
            };
            
            timer.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Optional: You can add real-time validation here
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Label click event - usually not needed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Determine which button was clicked - fix the null warning
            if (sender is Button clickedButton)
            {
                if (clickedButton.Name == "buttonGuess")
                {
                    ProcessGuess();
                }
                else if (clickedButton.Name == "button2")
                {
                    StartNewGame();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Label click event - usually not needed
        }

        // Add Enter key support for better user experience
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && txtGuess.Focused)
            {
                ProcessGuess();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
