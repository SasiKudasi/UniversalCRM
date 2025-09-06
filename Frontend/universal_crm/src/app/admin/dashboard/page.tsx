import Link from "next/link";
import { ListGroup } from "react-bootstrap";

export default function DashBoardPage() {
    return (
        <div className="container mt-4">
            <div className="row">
                <main className="col-md-8">
                    <h1>Добро пожаловать в дашборд</h1>
                    <p>
                        Здесь будет основной контент вашего админ-панели: таблицы, формы и
                        другая информация.
                    </p>
                </main>

                <aside className="col-md-4">
                    <h3>Панель управления</h3>
                    <ListGroup>
                        <Link href={"/admin/dashboard/create_new_page"}>Добавить страницу</Link>
                        <Link href={"/admin/dashboard/get_all_pages"}>Все страницы</Link>
                    </ListGroup>
                </aside>
            </div>
        </div>
    );
}
