import Link from "next/link";
import { ListGroup } from "react-bootstrap";
import Col from 'react-bootstrap/Col';

export default function AdminHeader() {
    return (
        <Col sm={3}>
            < aside >
                <h3>Панель управления</h3>
                <ListGroup>
                    <Link href={"/admin/dashboard"}>Главная</Link>
                    <Link href={"/admin/dashboard/create_new_page"}>Добавить страницу</Link>
                    <Link href={"/admin/dashboard/get_all_pages"}>Все страницы</Link>
                    <p> Заявки</p>
                    <p> Графики</p>
                </ListGroup>
            </aside >
        </Col>
    )
}