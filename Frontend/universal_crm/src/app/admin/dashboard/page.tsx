import { Container } from "react-bootstrap";
import AdminHeader from "./components/adminHeader";
export default function DashBoardPage() {
    return (
        <Container className="mt-4">
            <div className="row">
                <main className="col-md-8">
                    <h1>Добро пожаловать в дашборд</h1>
                    <p>
                        Здесь будет основной контент вашего админ-панели: таблицы, формы и
                        другая информация.
                    </p>
                </main>
                <div className="col-md-4">
                    <AdminHeader />
                </div>
            </div>
        </Container>

    );
}
