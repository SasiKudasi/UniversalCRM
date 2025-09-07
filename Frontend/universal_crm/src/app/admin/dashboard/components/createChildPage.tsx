import { api } from "@/lib/api";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { Button, Container, Form } from "react-bootstrap";
import { pageService } from '../services/pageService';
import { RootPage } from "../types/RootPage";

interface Page {
    id: string;
    title: string;
    path: string;
}

export const CreateChildPageForm = () => {
    const router = useRouter();

    const [pages, setPages] = useState<Page[]>([]);

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

    const [formData, setFormData] = useState<RootPage>({
        Title: '',
        Path: '',
        OrdinalNum: 0,
        Content: '',
        MetaTitle: '',
        MetaDescription: '',
        MetaKeywords: '',
        IsActive: true
    });
    const [parentPage, setParentPage] = useState('');
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);
    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value, type } = e.target;

        setFormData(prev => ({
            ...prev,
            [name]: type === 'checkbox' ? (e.target as HTMLInputElement).checked : value
        }));
    };
    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            var response = await pageService.createChild(parentPage, formData);
            console.log(response.status)
            if (response.status == 201) {
                router.push("/admin/dashboard/get_all_pages");
            }
        }
        catch (error: any) {
            setError(error.message);
        } finally {
            setLoading(false);
        }
    }


    // onSubmit={handleSubmit}
    return (
        <Container className="mt-4">
            <Form onSubmit={handleSubmit}>
                <Form.Select onChange={(e) => setParentPage(e.target.value)}>
                    {pages.map((page) => (
                        <option key={page.id} value={page.id}>
                            Имя: {page.title} | путь: {page.path}
                        </option>
                    ))}
                </Form.Select>
                <Form.Group className="mb-3">
                    <Form.Label>Title</Form.Label>
                    <Form.Control
                        type="text"
                        name="Title"
                        value={formData.Title}
                        required
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Path</Form.Label>
                    <Form.Control
                        type="text"
                        name="Path"
                        value={formData.Path}
                        required
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Порядковый номер</Form.Label>
                    <Form.Control
                        type="number"
                        name="OrdinalNum"
                        value={formData.OrdinalNum}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Content</Form.Label>
                    <Form.Control
                        as="textarea"
                        rows={5}
                        name="Content"
                        value={formData.Content}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Title</Form.Label>
                    <Form.Control
                        type="text"
                        name="MetaTitle"
                        value={formData.MetaTitle}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Description</Form.Label>
                    <Form.Control
                        as="textarea"
                        rows={3}
                        name="MetaDescription"
                        value={formData.MetaDescription}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Keywords</Form.Label>
                    <Form.Control
                        type="text"
                        name="MetaKeywords"
                        value={formData.MetaKeywords}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3" controlId="isActiveCheck">
                    <Form.Check
                        type="checkbox"
                        label="Активна?"
                        name="IsActive"
                        checked={formData.IsActive}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Button type="submit" variant="primary">
                    Создать
                </Button>
                {error && <p style={{ color: "red" }}>{error}</p>}
            </Form>
        </Container>
    )
}