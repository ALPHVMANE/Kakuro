<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leaderboard.aspx.cs" Inherits="Kakuro.Leaderboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leaderboard</title>

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css"
        rel="stylesheet" />

    <style>
        @import url('https://fonts.googleapis.com/css?family=Red+Hat+Display:400,900&display=swap');

        html, body {
            height: 100%;
            width: 100%;
            overflow-y: auto;
            background: #4B4168;
            color: #333;
            margin: 0;
            padding: 0;
        }

        .center {
            max-width: 900px;
            margin: 60px auto;
            padding-bottom: 60px;
            font-family: 'Red Hat Display', sans-serif;
        }

        .top3 {
            display: flex;
            justify-content: center;
            align-items: flex-end;
            color: #4B4168;
        }

            .top3 .item {
                box-sizing: border-box;
                position: relative;
                background: white;
                width: 9rem;
                height: 10rem;
                text-align: center;
                padding: 2.8rem 0 0;
                margin: 1rem 1rem 2rem;
                border-radius: 0.5rem;
                transform-origin: bottom;
                cursor: pointer;
                transition: transform 200ms ease-in-out;
                box-shadow: 0 0 4rem 0 rgba(0,0,0,0.1), 0 1rem 2rem -1rem rgba(0,0,0,0.3);
            }

                .top3 .item .pic {
                    position: absolute;
                    top: -2rem;
                    left: 2.5rem;
                    width: 4rem;
                    height: 4rem;
                    border-radius: 50%;
                    background-size: cover;
                    background-position: center;
                    margin-right: 1rem;
                    box-shadow: 0 0 1rem 0 rgba(0,0,0,0.2), 0 1rem 1rem -0.5rem rgba(0,0,0,0.3);
                }

                .top3 .item .pos {
                    font-weight: 900;
                    font-size: 1.5rem;
                    margin-bottom: 0.5rem;
                }

                .top3 .item .name {
                    font-size: 1.1rem;
                    margin-bottom: 0.5rem;
                }

                .top3 .item .score {
                    opacity: 0.5;
                }

                    .top3 .item .score::after {
                        display: block;
                        content: 'points';
                        opacity: 0.5;
                    }

                .top3 .item.one {
                    width: 10rem;
                    height: 11rem;
                    padding-top: 3.5rem;
                }

                    .top3 .item.one .pic {
                        width: 5rem;
                        height: 5rem;
                        left: 2.5rem;
                    }

                .top3 .item:hover {
                    transform: scale(1.05);
                }

        .list {
            padding-left: 2rem;
            margin: 0 auto;
        }

            .list .item {
                position: relative;
                display: flex;
                align-items: center;
                height: 3rem;
                border-radius: 4rem;
                margin-bottom: 2rem;
                background: #EAA786;
                transform-origin: left;
                cursor: pointer;
                transition: transform 200ms ease-in-out;
                box-shadow: 0 0 4rem 0 rgba(0,0,0,0.1), 0 1rem 2rem -1rem rgba(0,0,0,0.3);
            }

                .list .item .pos {
                    font-weight: 900;
                    position: absolute;
                    left: -2rem;
                    text-align: center;
                    font-size: 1.25rem;
                    width: 1.5rem;
                    color: white;
                    opacity: 0.6;
                    transition: opacity 200ms ease-in-out;
                }

                .list .item .pic {
                    width: 4rem;
                    height: 4rem;
                    border-radius: 50%;
                    background-size: cover;
                    background-position: center;
                    margin-right: 1rem;
                    box-shadow: 0 0 1rem 0 rgba(0,0,0,0.2), 0 1rem 1rem -0.5rem rgba(0,0,0,0.3);
                }

                .list .item .name {
                    flex-grow: 2;
                    flex-basis: 10rem;
                    font-size: 1.1rem;
                }

                .list .item .score {
                    margin-right: 1.5rem;
                    opacity: 0.5;
                }

                    .list .item .score::after {
                        margin-right: 1rem;
                        content: 'points';
                        opacity: 0.5;
                    }

                .list .item:hover {
                    transform: scale(1.05);
                }

                    .list .item:hover .pos {
                        opacity: 0.8;
                    }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <nav class="navbar navbar-expand-lg bg-body-tertiary" data-bs-theme="dark">
            <div class="container-fluid">
                <a class="navbar-brand text-decoration-underline">Kakuro</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDropdown"
                    aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNavDropdown">
                    <ul class="navbar-nav">

                        <li class="nav-item">
                            <a class="nav-link" aria-current="page" href="">Home</a>
                        </li>

                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page" href="Leaderboard.aspx">Leaderboard</a>
                        </li>

                        <li class="nav-item">
                            <a class="nav-link" aria-current="page" href="">Logout</a>
                        </li>

                    </ul>
                </div>
            </div>
        </nav>

        <div>
        </div>

        <div class="center">
            <div class="top3">
                <div class="two item" style="background-color: silver">
                    <div class="pos">2</div>
                    <div class="name">Name</div>
                    <div class="score">6453</div>
                </div>

                <div class="one item" style="background-color: gold">
                    <div class="pos">1</div>
                    <div class="name">Name</div>
                    <div class="score">6794</div>
                </div>

                <div class="three item" style="background-color: sienna">
                    <div class="pos">3</div>
                    <div class="name">Name</div>
                    <div class="score">6034</div>
                </div>
            </div>

            <div class="list">
                <div class="item">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="pos">4</div>
                    <div class="name">Name</div>
                    <div class="score">5980</div>
                </div>

                <div class="item">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="pos">5</div>
                    <div class="name">Name</div>
                    <div class="score">5978</div>
                </div>

                <div class="item">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="pos">6</div>
                    <div class="name">Name</div>
                    <div class="score">5845</div>
                </div>

                <div class="item">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="pos">7</div>
                    <div class="name">Name</div>
                    <div class="score">5799</div>
                </div>

                <div class="item">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="pos">8</div>
                    <div class="name">Name</div>
                    <div class="score">5756</div>
                </div>

                <div class="item">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="pos">9</div>
                    <div class="name">Name</div>
                    <div class="score">5713</div>
                </div>

                <div class="item">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="pos">10</div>
                    <div class="name">Name</div>
                    <div class="score">5674</div>
                </div>
            </div>
        </div>

    </form>

    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

</body>
</html>
