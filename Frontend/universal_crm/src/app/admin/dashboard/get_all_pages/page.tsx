'use client';
import { api } from "@/lib/api";
import { useEffect, useState } from "react";
import { Button, Row, Table } from "react-bootstrap";
import Col from 'react-bootstrap/Col';
import AdminHeader from "../components/adminHeader";
import { pageService } from '../services/pageService';

interface Page {
    id: string;
    title: string;
    path: string;
}

export default function GetAllPages() {
    const [pages, setPages] = useState<Page[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        api.get("/admin/all_page", {
            headers: { Accept: "application/json" },
            withCredentials: true,
        })
            .then(res => {
                setPages(res.data);
                setLoading(false);
            })
            .catch(err => {
                console.error(err);
                setLoading(false);
            });
    }, []);

    async function handleDelete(id: string) {
        const response = await pageService.delete(id);
        if (response.status == 200) {
            window.location.reload();
        }
    }

    if (loading) return <p>Загрузка...</p>;

    return (
        <div className="container mt-4">
            <h1>Страницы</h1>
            <Row>
                <Col sm={8}>
                    <Table striped bordered hover responsive>
                        <thead>
                            <tr>
                                <th>Путь</th>
                                <th>H1</th>
                                <th>Действия</th>
                            </tr>
                        </thead>
                        <tbody>
                            {pages.map((page) => (
                                <tr key={page.id}>
                                    <td>{page.path}</td>
                                    <td>{page.title}</td>
                                    <td>
                                        <Button
                                            variant="info"
                                            size="sm"
                                            className="me-2"
                                            onClick={() => window.open("/" + page.path, "_blank")}
                                        >
                                            Посетить
                                        </Button>
                                        <Button
                                            variant="warning"
                                            size="sm"
                                            onClick={() => window.location.href = "/admin/dashboard/updatePage/" + page.id}
                                        >
                                            Обновить
                                        </Button>
                                        <Button
                                            size="sm"
                                            variant="danger"
                                            onClick={() => handleDelete(page.id)}
                                        >
                                            Удалить
                                        </Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
                <AdminHeader />
            </Row>
        </div>
    );
}
