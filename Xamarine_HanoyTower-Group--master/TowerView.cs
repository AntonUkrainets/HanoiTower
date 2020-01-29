using CoreGraphics;
using System;
using UIKit;
using System.Threading.Tasks;
using System.Threading;

namespace XamarinAnimationIOS_HanoyTower_
{
    public partial class TowerView : UIViewController
    {
        private bool _isAnimationStarted;
        private bool _isAnimationPaused;

        private CancellationTokenSource _cancellationSource;

        private nfloat _animationDelay = 1000;

        public int DisksAmount { get; set; }

        private UIView _viewTowers;

        private Rod _rodA;
        private Rod _rodB;
        private Rod _rodC;

        public TowerView(IntPtr handle)
            : base(handle)
        {
            _isAnimationStarted = false;
            _isAnimationPaused = false;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            _viewTowers = _viewForTowers;

            InitDisks();

            _isAnimationStarted = true;
        }

        private void InitDisks()
        {
            CreateRods();
            CreateDisksViews();
        }

        partial void StartButton_Tapped(UIButton sender)
        {
            if (!_isAnimationStarted)
            {
                foreach (var subView in _viewTowers.Subviews)
                {
                    var isButton = subView is UIButton;
                    if (!isButton)
                        subView.RemoveFromSuperview();
                }

                InitDisks();
            }

            _cancellationSource = new CancellationTokenSource();
            SolvePuzzle(_cancellationSource.Token);
        }

        partial void PauseButton_Tapped(UIButton sender)
        {
            _isAnimationPaused = !_isAnimationPaused;

            if (_isAnimationPaused)
                sender.SetTitle("Continue", UIControlState.Normal);
            else
                sender.SetTitle("Pause", UIControlState.Normal);
        }

        partial void StopButton_Tapped(UIButton sender)
        {
            _cancellationSource?.Cancel();

            _isAnimationStarted = false;
        }

        partial void ReverseButton_Tapped(UIButton sender)
        {
            _cancellationSource = new CancellationTokenSource();
            BackwardsSolvePuzzle(_cancellationSource.Token);
        }

        private void CreateRods()
        {
            var rodCoordinateY = _viewTowers.Frame.Height * 0.9f;
            var rodACoordinateX = _viewTowers.Frame.Width * 0.2f;

            _rodA = new Rod(
                rodACoordinateX,
                rodCoordinateY
            );

            var rodBCoordinateX = _viewTowers.Frame.Width * 0.5f;
            _rodB = new Rod(
                rodBCoordinateX,
                rodCoordinateY
            );

            var rodCCoordinateX = _viewTowers.Frame.Width * 0.8f;
            _rodC = new Rod(
                rodCCoordinateX,
                rodCoordinateY
            );
        }

        private void CreateDisksViews()
        {
            var normalDiskWidth = _viewTowers.Frame.Width / 8f;
            var normalDiskHeight = _viewTowers.Frame.Height / DisksAmount * 0.8f;

            var diskHeight = normalDiskHeight;

            for (int i = 0; i < DisksAmount; i++)
            {
                var diskWidth = normalDiskWidth - ((normalDiskWidth / DisksAmount) * i);

                var diskCoordinateX = _rodA.X - (diskWidth / 2);
                var diskCoordinateY = _rodA.Y - diskHeight * (i + 1);

                var diskView = CreateDiskView(diskCoordinateX, diskCoordinateY, diskWidth, diskHeight);

                _rodA.Disks.Push(diskView);
                _viewTowers.AddSubview(diskView);
            }
        }

        private UIView CreateDiskView(
            nfloat diskCoordinateX,
            nfloat diskCoordinateY,
            nfloat diskWidth,
            nfloat diskHeight
        )
        {
            var rect = new CGRect(
                diskCoordinateX,
                diskCoordinateY,
                diskWidth,
                diskHeight
            );

            var diskView = new UIView(rect)
            {
                BackgroundColor = GetRandomColor()
            };

            return diskView;
        }

        private UIColor GetRandomColor()
        {
            var random = new Random();

            var color = UIColor.FromRGB(
                random.Next(255),
                random.Next(255),
                random.Next(255)
            );

            return color;
        }

        public async Task SolvePuzzle(CancellationToken cancellationToken)
        {
            await NextTurn(
                DisksAmount,
                _rodA,
                _rodB,
                _rodC,
                cancellationToken
            );
        }

        public async Task BackwardsSolvePuzzle(CancellationToken cancellationToken)
        {
            await NextTurn(
                DisksAmount,
                _rodB,
                _rodA,
                _rodC,
                cancellationToken
            );
        }

        private async Task NextTurn(
            int disksAmount,
            Rod source,
            Rod dest,
            Rod spare,
            CancellationToken cancellationToken
        )
        {
            if (disksAmount > 0)
            {
                await NextTurn(
                    disksAmount - 1,
                    source,
                    spare,
                    dest,
                    cancellationToken
                );

                await Task.Delay((int)_animationDelay);

                if (_cancellationSource.Token.IsCancellationRequested)
                    return;

                while (_isAnimationPaused)
                    await Task.Delay(100);

                await MoveDiskAsync(source, dest);

                await NextTurn(
                    disksAmount - 1,
                    spare,
                    dest,
                    source,
                    cancellationToken
                );

            }
        }

        private async Task MoveDiskAsync(
            Rod fromRod,
            Rod toRod
        )
        {
            var towerView = fromRod.Disks.Pop();

            UIView.Animate(
                _animationDelay / 1000,
                () =>
                {
                    var normalDiskHeight = _viewTowers.Frame.Height / DisksAmount * 0.8f;
                    var y = _viewTowers.Frame.Height * 0.9f - toRod.Disks.Count * normalDiskHeight;

                    var x = toRod.X - towerView.Frame.Width / 2;

                    towerView.Frame = new CGRect(
                        x,
                        y - towerView.Frame.Height,
                        towerView.Frame.Width,
                        towerView.Frame.Height);
                });

            toRod.Disks.Push(towerView);
        }
    }
}